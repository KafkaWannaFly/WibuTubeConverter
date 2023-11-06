"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const electron_1 = require("electron");
const path_1 = __importDefault(require("path"));
// Handle creating/removing shortcuts on Windows when installing/uninstalling.
if (require('electron-squirrel-startup')) {
    electron_1.app.quit();
}
const createWindow = () => {
    // Create the browser window.
    const mainWindow = new electron_1.BrowserWindow({
        width: 800,
        height: 600,
        webPreferences: {
            preload: path_1.default.join(__dirname, 'preload.js'),
        },
    });
    // and load the index.html of the app.
    if (MAIN_WINDOW_VITE_DEV_SERVER_URL) {
        mainWindow.loadURL(MAIN_WINDOW_VITE_DEV_SERVER_URL);
    }
    else {
        mainWindow.loadFile(path_1.default.join(__dirname, `../renderer/${MAIN_WINDOW_VITE_NAME}/index.html`));
    }
    // Open the DevTools.
    // mainWindow.webContents.openDevTools({
    //   mode: "detach"
    // });
    setInterval(() => {
        console.log('👋 This message is being logged by "main.ts", included via Vite.');
    }, 2000);
};
// This method will be called when Electron has finished
// initialization and is ready to create browser windows.
// Some APIs can only be used after this event occurs.
electron_1.app.on('ready', createWindow);
// Quit when all windows are closed, except on macOS. There, it's common
// for applications and their menu bar to stay active until the user quits
// explicitly with Cmd + Q.
electron_1.app.on('window-all-closed', () => {
    if (process.platform !== 'darwin') {
        electron_1.app.quit();
    }
});
electron_1.app.on('activate', () => {
    // On OS X it's common to re-create a window in the app when the
    // dock icon is clicked and there are no other windows open.
    if (electron_1.BrowserWindow.getAllWindows().length === 0) {
        createWindow();
    }
});
// In this file you can include the rest of your app's specific main process
// code. You can also put them in separate files and import them here.
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibWFpbi5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbIi4uL3NyYy9tYWluLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7Ozs7O0FBQUEsdUNBQThDO0FBQzlDLGdEQUF3QjtBQUV4Qiw4RUFBOEU7QUFDOUUsSUFBSSxPQUFPLENBQUMsMkJBQTJCLENBQUMsRUFBRTtJQUN4QyxjQUFHLENBQUMsSUFBSSxFQUFFLENBQUM7Q0FDWjtBQUVELE1BQU0sWUFBWSxHQUFHLEdBQUcsRUFBRTtJQUN4Qiw2QkFBNkI7SUFDN0IsTUFBTSxVQUFVLEdBQUcsSUFBSSx3QkFBYSxDQUFDO1FBQ25DLEtBQUssRUFBRSxHQUFHO1FBQ1YsTUFBTSxFQUFFLEdBQUc7UUFDWCxjQUFjLEVBQUU7WUFDZCxPQUFPLEVBQUUsY0FBSSxDQUFDLElBQUksQ0FBQyxTQUFTLEVBQUUsWUFBWSxDQUFDO1NBQzVDO0tBQ0YsQ0FBQyxDQUFDO0lBRUgsc0NBQXNDO0lBQ3RDLElBQUksK0JBQStCLEVBQUU7UUFDbkMsVUFBVSxDQUFDLE9BQU8sQ0FBQywrQkFBK0IsQ0FBQyxDQUFDO0tBQ3JEO1NBQU07UUFDTCxVQUFVLENBQUMsUUFBUSxDQUFDLGNBQUksQ0FBQyxJQUFJLENBQUMsU0FBUyxFQUFFLGVBQWUscUJBQXFCLGFBQWEsQ0FBQyxDQUFDLENBQUM7S0FDOUY7SUFFRCxxQkFBcUI7SUFDckIsd0NBQXdDO0lBQ3hDLG1CQUFtQjtJQUNuQixNQUFNO0lBRU4sV0FBVyxDQUFDLEdBQUcsRUFBRTtRQUNmLE9BQU8sQ0FBQyxHQUFHLENBQUMsa0VBQWtFLENBQUMsQ0FBQztJQUNsRixDQUFDLEVBQUUsSUFBSSxDQUFDLENBQUE7QUFDVixDQUFDLENBQUM7QUFFRix3REFBd0Q7QUFDeEQseURBQXlEO0FBQ3pELHNEQUFzRDtBQUN0RCxjQUFHLENBQUMsRUFBRSxDQUFDLE9BQU8sRUFBRSxZQUFZLENBQUMsQ0FBQztBQUU5Qix3RUFBd0U7QUFDeEUsMEVBQTBFO0FBQzFFLDJCQUEyQjtBQUMzQixjQUFHLENBQUMsRUFBRSxDQUFDLG1CQUFtQixFQUFFLEdBQUcsRUFBRTtJQUMvQixJQUFJLE9BQU8sQ0FBQyxRQUFRLEtBQUssUUFBUSxFQUFFO1FBQ2pDLGNBQUcsQ0FBQyxJQUFJLEVBQUUsQ0FBQztLQUNaO0FBQ0gsQ0FBQyxDQUFDLENBQUM7QUFFSCxjQUFHLENBQUMsRUFBRSxDQUFDLFVBQVUsRUFBRSxHQUFHLEVBQUU7SUFDdEIsZ0VBQWdFO0lBQ2hFLDREQUE0RDtJQUM1RCxJQUFJLHdCQUFhLENBQUMsYUFBYSxFQUFFLENBQUMsTUFBTSxLQUFLLENBQUMsRUFBRTtRQUM5QyxZQUFZLEVBQUUsQ0FBQztLQUNoQjtBQUNILENBQUMsQ0FBQyxDQUFDO0FBRUgsNEVBQTRFO0FBQzVFLHNFQUFzRSIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7IGFwcCwgQnJvd3NlcldpbmRvdyB9IGZyb20gJ2VsZWN0cm9uJztcbmltcG9ydCBwYXRoIGZyb20gJ3BhdGgnO1xuXG4vLyBIYW5kbGUgY3JlYXRpbmcvcmVtb3Zpbmcgc2hvcnRjdXRzIG9uIFdpbmRvd3Mgd2hlbiBpbnN0YWxsaW5nL3VuaW5zdGFsbGluZy5cbmlmIChyZXF1aXJlKCdlbGVjdHJvbi1zcXVpcnJlbC1zdGFydHVwJykpIHtcbiAgYXBwLnF1aXQoKTtcbn1cblxuY29uc3QgY3JlYXRlV2luZG93ID0gKCkgPT4ge1xuICAvLyBDcmVhdGUgdGhlIGJyb3dzZXIgd2luZG93LlxuICBjb25zdCBtYWluV2luZG93ID0gbmV3IEJyb3dzZXJXaW5kb3coe1xuICAgIHdpZHRoOiA4MDAsXG4gICAgaGVpZ2h0OiA2MDAsXG4gICAgd2ViUHJlZmVyZW5jZXM6IHtcbiAgICAgIHByZWxvYWQ6IHBhdGguam9pbihfX2Rpcm5hbWUsICdwcmVsb2FkLmpzJyksXG4gICAgfSxcbiAgfSk7XG5cbiAgLy8gYW5kIGxvYWQgdGhlIGluZGV4Lmh0bWwgb2YgdGhlIGFwcC5cbiAgaWYgKE1BSU5fV0lORE9XX1ZJVEVfREVWX1NFUlZFUl9VUkwpIHtcbiAgICBtYWluV2luZG93LmxvYWRVUkwoTUFJTl9XSU5ET1dfVklURV9ERVZfU0VSVkVSX1VSTCk7XG4gIH0gZWxzZSB7XG4gICAgbWFpbldpbmRvdy5sb2FkRmlsZShwYXRoLmpvaW4oX19kaXJuYW1lLCBgLi4vcmVuZGVyZXIvJHtNQUlOX1dJTkRPV19WSVRFX05BTUV9L2luZGV4Lmh0bWxgKSk7XG4gIH1cblxuICAvLyBPcGVuIHRoZSBEZXZUb29scy5cbiAgLy8gbWFpbldpbmRvdy53ZWJDb250ZW50cy5vcGVuRGV2VG9vbHMoe1xuICAvLyAgIG1vZGU6IFwiZGV0YWNoXCJcbiAgLy8gfSk7XG5cbiAgc2V0SW50ZXJ2YWwoKCkgPT4ge1xuICAgIGNvbnNvbGUubG9nKCfwn5GLIFRoaXMgbWVzc2FnZSBpcyBiZWluZyBsb2dnZWQgYnkgXCJtYWluLnRzXCIsIGluY2x1ZGVkIHZpYSBWaXRlLicpO1xuICB9LCAyMDAwKVxufTtcblxuLy8gVGhpcyBtZXRob2Qgd2lsbCBiZSBjYWxsZWQgd2hlbiBFbGVjdHJvbiBoYXMgZmluaXNoZWRcbi8vIGluaXRpYWxpemF0aW9uIGFuZCBpcyByZWFkeSB0byBjcmVhdGUgYnJvd3NlciB3aW5kb3dzLlxuLy8gU29tZSBBUElzIGNhbiBvbmx5IGJlIHVzZWQgYWZ0ZXIgdGhpcyBldmVudCBvY2N1cnMuXG5hcHAub24oJ3JlYWR5JywgY3JlYXRlV2luZG93KTtcblxuLy8gUXVpdCB3aGVuIGFsbCB3aW5kb3dzIGFyZSBjbG9zZWQsIGV4Y2VwdCBvbiBtYWNPUy4gVGhlcmUsIGl0J3MgY29tbW9uXG4vLyBmb3IgYXBwbGljYXRpb25zIGFuZCB0aGVpciBtZW51IGJhciB0byBzdGF5IGFjdGl2ZSB1bnRpbCB0aGUgdXNlciBxdWl0c1xuLy8gZXhwbGljaXRseSB3aXRoIENtZCArIFEuXG5hcHAub24oJ3dpbmRvdy1hbGwtY2xvc2VkJywgKCkgPT4ge1xuICBpZiAocHJvY2Vzcy5wbGF0Zm9ybSAhPT0gJ2RhcndpbicpIHtcbiAgICBhcHAucXVpdCgpO1xuICB9XG59KTtcblxuYXBwLm9uKCdhY3RpdmF0ZScsICgpID0+IHtcbiAgLy8gT24gT1MgWCBpdCdzIGNvbW1vbiB0byByZS1jcmVhdGUgYSB3aW5kb3cgaW4gdGhlIGFwcCB3aGVuIHRoZVxuICAvLyBkb2NrIGljb24gaXMgY2xpY2tlZCBhbmQgdGhlcmUgYXJlIG5vIG90aGVyIHdpbmRvd3Mgb3Blbi5cbiAgaWYgKEJyb3dzZXJXaW5kb3cuZ2V0QWxsV2luZG93cygpLmxlbmd0aCA9PT0gMCkge1xuICAgIGNyZWF0ZVdpbmRvdygpO1xuICB9XG59KTtcblxuLy8gSW4gdGhpcyBmaWxlIHlvdSBjYW4gaW5jbHVkZSB0aGUgcmVzdCBvZiB5b3VyIGFwcCdzIHNwZWNpZmljIG1haW4gcHJvY2Vzc1xuLy8gY29kZS4gWW91IGNhbiBhbHNvIHB1dCB0aGVtIGluIHNlcGFyYXRlIGZpbGVzIGFuZCBpbXBvcnQgdGhlbSBoZXJlLlxuIl19