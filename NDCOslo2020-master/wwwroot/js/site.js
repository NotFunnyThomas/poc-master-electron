// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
(function () {
    const { ipcRenderer } = require("electron");

    /*
    document.getElementById("async-msg").addEventListener("click", () => {
        ipcRenderer.send("async-msg", 'ping');
    });
    */

    ipcRenderer.on('asynchronous-reply', (event, arg) => {
        const message = `Asynchronous message reply: ${arg}`;
        document.getElementById('async-reply').innerHTML = message;
    });

    /*
    document.getElementById("sync-msg").addEventListener("click", () => {
        const reply = ipcRenderer.sendSync("sync-msg", "ping");
        const message = `Synchronous message reply: ${reply}`;
        document.getElementById('sync-reply').innerHTML = message;
    });
    */

}());