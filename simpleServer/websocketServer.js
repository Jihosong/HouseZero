var ws = require('websocket.io');
var http = require('http');

var server = ws.listen(6789, {clientTracking: true});

server.on('connection', function (socket) {
    console.log("CONNECTION");

    socket.on('message', function (data) { 
        console.log("message");
        console.log(data);
        var msg = JSON.parse(data);
        console.log(msg.message);
    //socket.send(JSON.stringify({message: "hello back!"}))
        //socket.send(JSON.stringify({message: {type: "temperature", data: 23}}));
      //broadcast({message: 'hello'}, server, socket);
      sendSampleData();
  });
    socket.on('close', function () {
        console.log("closed connection");
    });
    socket.on('error', function(err) {
        console.log(err);
    });
});

//setInterval(getUpdateSensors, 90000);
setInterval(sendSampleData, 10000);


function broadcast (message, server, sender) {
    server.clients.forEach(function(client) {
        try {
            if (client != sender) {
                client.send(JSON.stringify(message));
            }
        }
        catch (e) {
            console.log(e);
        }
    })
}

function sendSampleData() {
    var data = updateSampleData();
    if (server.clientsCount > 0) {
        server.clients.forEach(function(client) {
            broadcast(data, server);
        })
    }
}

function updateSampleData() {
    var outData = {message: []}
    sampleData.message.forEach(function(d) {
        var sd = {type: d.type, id: d.id};
        if (sd.id.indexOf("temperature") > -1) {
            sd.data = Math.random() < 0.82 ? d.data : Math.random()*0.4  - 0.2 + d.data;
            sd.data = Math.round(sd.data * 10) / 10.0;
        }
        else if (sd.id.indexOf("humidity") > -1) {
            sd.data = Math.random() < 0.82 ? d.data : Math.random() * 0.08 * d.data - 0.04 * d.data + d.data;
            sd.data = Math.round(sd.data);
        }
        outData.message.push(sd);
    });
    return outData;
}

var sampleData = {message: [{type: 'sensor', id: 'temperature1', data: 21}, 
{type: 'sensor', id: 'temperature2', data: 22.5}, {type: 'sensor', id: 'temperature3', data: 21},
{type: 'sensor', id: 'temperature4', data: 20.5}, {type: 'sensor', id: 'humidity1', data: 45},
{type: 'sensor', id: 'humidity2', data: 48}]};

function getUpdateSensors() {
    http.get('http://172.22.129.49:9000/get/sensorsData_placeHolder', (res) => {
        const { statusCode } = res;
        const contentType = res.headers['content-type'];

        let error;
        if (statusCode !== 200) {
            error = new Error('Request Failed.\n' +
                `Status Code: ${statusCode}`);
        } else if (!/^application\/json/.test(contentType)) {
            error = new Error('Invalid content-type.\n' +
                `Expected application/json but received ${contentType}`);
        }
        if (error) {
            console.error(error.message);
      // consume response data to free up memory
      res.resume();
      return;
  }

  res.setEncoding('utf8');
  let rawData = '';
  res.on('data', (chunk) => { rawData += chunk; });
  res.on('end', () => {
    try {
        const parsedData = JSON.parse(rawData);
        console.log(parsedData);
        //send data to HoloLens
        var message = {};
        message.message = parsedData.map(function(item) { 
            return {type: 'sensor', id: item.sensor, data: item.data};
        });
        if (server.clientsCount > 0) {
            clients.forEach(function(client) {
                broadcast(message, server);
            })
        }
    } catch (e) {
        console.error(e.message);
    }
});
}).on('error', (e) => {
    console.error(`Got error: ${e.message}`);
});
}

////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////
process.on('SIGTERM', function() {
    shutdown('SIGTERM');
});
process.on('SIGINT', function() {
    shutdown('SIGINT');
});
// process.on('exit', function() {
//   shutdown('exit');
// });
process.on('SIGUSR1', function() {
    shutdown('SIGUSR1');
});
process.on('SIGUSR2', function() {
    shutdown('SIGUSR2');
});
/*process.on('uncaughtException', function() {
  shutdown('uncaughtException');
});*/

function shutdown(reason) {
    console.log("Got " + reason + ". Will shutdown.")
    server.clients.forEach(function(client) {
        if (!!client) client.close(0);
    })
    setTimeout(process.exit, 200);
}





