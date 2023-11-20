export const utils = {
    isValidUrl: (url: string) => {
        try {
            new URL(url);
            return true;
        } catch (e) {
            return false;
        }
    },
    formatTime: (seconds: number) => {
        const minutes = Math.floor(seconds / 60);
        const remainingSeconds = seconds % 60;

        const formattedMinutes = minutes.toString().padStart(2, "0");
        const formattedSeconds = remainingSeconds.toString().padStart(2, "0");

        return `${formattedMinutes}:${formattedSeconds}`;
    },
};
