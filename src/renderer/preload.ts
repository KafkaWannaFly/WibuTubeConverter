import { contextBridge, ipcRenderer } from "electron";

contextBridge.exposeInMainWorld("api", {
    ipcRenderer: ipcRenderer,
});

declare global {
    interface Window {
        api: {
            ipcRenderer: Electron.IpcRenderer;
        };
    }
}
