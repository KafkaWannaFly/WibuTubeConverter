export const musicEndPoint = (ipcMain: Electron.IpcMain) => {
    ipcMain.handle("get-song", async (event, arg) => {
        console.log(arg);
        return "pong";
    });
};
