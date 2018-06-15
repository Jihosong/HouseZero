using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if WINDOWS_UWP
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Data.Json;
#endif

public class WebSockets : MonoBehaviour {

    //public static WebSockets Instance;

	// Use this for initialization
	void Start () {
		Debug.Log("will initialize web socket");
        //Instance = this;
        timePrev = Time.time + 10000f;
		Initialize ();
	}

    // Update is called once per frame
    private float timePrev;
	void Update () {
        /*if (!socketConnected && Time.time - timePrev > 5000f)
        {
            this.Initialize();
            timePrev = Time.time;
        }*/
	}

    private bool socketConnected = false;

	#if WINDOWS_UWP
	private Windows.Networking.Sockets.MessageWebSocket messageWebSocket;
	#endif

	protected void Initialize()
	{
#if WINDOWS_UWP
		this.messageWebSocket = new Windows.Networking.Sockets.MessageWebSocket();

        // In this example, we send/receive a string, so we need to set the MessageType to Utf8.
        this.messageWebSocket.Control.MessageType = Windows.Networking.Sockets.SocketMessageType.Utf8;

        this.messageWebSocket.MessageReceived += WebSocket_MessageReceived;
        this.messageWebSocket.Closed += WebSocket_Closed;

        try
        {
            Debug.Log("will connect");
            Task connectTask = this.messageWebSocket.ConnectAsync(new Uri("ws://52.90.235.79:6789")).AsTask();
            connectTask.ContinueWith(_ => this.Send("message", "hello")); //this.SendMessageUsingMessageWebSocketAsync("{\"message\": \"hello\"}"));
            //socketConnected = true;
            //Debug.Log("now connected.");
        }
        catch (Exception ex)
        {
            Windows.Web.WebErrorStatus webErrorStatus = Windows.Networking.Sockets.WebSocketError.GetStatus(ex.GetBaseException().HResult);
            // Add additional code here to handle exceptions.
            Debug.Log(ex);
        }
#endif
    }

#if WINDOWS_UWP

        public void Send(String keyString, String valueString)
        {
            JsonObject message = new JsonObject();
            message.SetNamedValue(keyString, JsonValue.CreateStringValue(valueString));
            this.Send(message);
        }

        public async void Send(JsonObject jsonMessage)
        {
            await this.SendMessageUsingMessageWebSocketAsync(jsonMessage.ToString());
        }

    private async Task SendMessageUsingMessageWebSocketAsync(string message)
        {
            using (var dataWriter = new DataWriter(this.messageWebSocket.OutputStream))
            {
                try
                {
                    dataWriter.WriteString(message);




                await dataWriter.StoreAsync();
                    dataWriter.DetachStream();
                    dataWriter.Dispose();
                }
                catch (Exception e)
                {
                    Debug.Log("exception thrown: " + e);
                    Debug.Log(e.Message);

                    //close the websocket
                    this.messageWebSocket.Close(1006, e.Message);
                    //create a new one and reconnect
                    this.Initialize();
                return;
                }
            }
            Debug.Log("Sending message using MessageWebSocket: " + message);
        }

        private void WebSocket_MessageReceived(Windows.Networking.Sockets.MessageWebSocket sender, Windows.Networking.Sockets.MessageWebSocketMessageReceivedEventArgs args)
        {
            Debug.Log("message received");
            try
            {
                using (DataReader dataReader = args.GetDataReader())
                {
                    dataReader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                    string messageString = dataReader.ReadString(dataReader.UnconsumedBufferLength);
                    JsonArray msgcontent = JsonObject.Parse(messageString).GetNamedArray("message");
                    Debug.Log("Message received from MessageWebSocket: " + msgcontent);
                    //this.messageWebSocket.Dispose(); //this closes the websocket
                    //do something with the message
                    try
                    {
                        foreach(IJsonValue message in msgcontent) {
                            if (message.ValueType == JsonValueType.Object)
                            {
                            JsonObject messageObject = message.GetObject();
                                if (messageObject.GetNamedString("type") == "sensor") //{message: [{ type: 'sensor', id: 'temp1', data: '23.5'},
                                {                                              //           { type: 'UIaction', id: 'ventilation', data: 'on'}]}
                                    //update sensor with the received data
                                    string sensor = messageObject.GetNamedString("id");
                                    double data = messageObject.GetNamedValue("data").GetNumber();
                                    UpdateStuff.Instance.UpdateSensor(sensor, data);
                                }
                                else if (messageObject.GetNamedString("type") == "UIaction")
                                {
                                    //send action to buttons
                                    string button = messageObject.GetNamedString("id");
                                    string data = messageObject.GetNamedString("data");
                                    UpdateStuff.Instance.UpdateButton(button, data);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        //this should catch bad format messages or Unity related errors
                        Debug.Log(e.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Windows.Web.WebErrorStatus webErrorStatus = Windows.Networking.Sockets.WebSocketError.GetStatus(ex.GetBaseException().HResult);
                Debug.Log(ex);
                //close the websocket
                this.messageWebSocket.Close(1006, ex.Message);
                //create a new one and reconnect
                this.Initialize();
            }
        }

        private void OnDisconnect()
        {
            Debug.Log("disconneted");
        }

        private void WebSocket_Closed(Windows.Networking.Sockets.IWebSocket sender, Windows.Networking.Sockets.WebSocketClosedEventArgs args)
        {
            Debug.Log("WebSocket_Closed; Code: " + args.Code + ", Reason: \"" + args.Reason + "\"");
            // Add additional code here to handle the WebSocket being closed.
            if (this.messageWebSocket == sender)
            {
                CloseSocket();
            }
            socketConnected = false;
        }

        private void CloseSocket()
        {
            if (this.messageWebSocket != null)
            {
                try
                {
                    this.messageWebSocket.Close(1000, "Closed due to user request.");
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
                this.messageWebSocket = null;
            }
        }
#endif

}
