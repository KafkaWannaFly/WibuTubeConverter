export const utils = {
    isValidUrl: (url: string) => {
        try {
            new URL(url);
            return true;
        } catch (e) {
            return false;
        }
    },
};
