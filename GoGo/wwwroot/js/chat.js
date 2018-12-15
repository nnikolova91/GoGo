function SendMessage(id) {
    var inputBoxId = id.concat("inputMessage");
    var message = document.getElementById(inputBoxId).value
    connection.invoke("SendMessage", id, message).catch(err => console.error(err.toString()));
}

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("ReceiveMessage", (user, message) => {
    const encodedMsg = user + " says " + message;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.onclose(async () => {
    await start();
});

start();

async function start() {
    try {
        await connection.start();
        console.log('connected');
    } catch (err) {
        console.log(err);
        setTimeout(() => start(), 5000);
    }
};