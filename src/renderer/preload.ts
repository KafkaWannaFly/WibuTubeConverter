import { contextBridge, ipcRenderer } from "electron";

contextBridge.exposeInMainWorld("api", {
    ipcRenderer: ipcRenderer,
    on: (
        channel: string,
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        listener: (event: Electron.IpcRendererEvent, ...args: any[]) => void
    ) => ipcRenderer.on(channel, listener),
});

declare global {
    interface Window {
        api: {
            ipcRenderer: Electron.IpcRenderer;
            on: (
                channel: string,
                // eslint-disable-next-line @typescript-eslint/no-explicit-any
                listener: (event: Electron.IpcRendererEvent, ...args: any[]) => void
            ) => void;
        };
    }
}
