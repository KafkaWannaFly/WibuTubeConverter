import { action, makeObservable, observable } from "mobx";

export class SearchStore {
    @observable url = "";

    constructor() {
        makeObservable(this);
    }

    @action
    set setUrl(url: string) {
        this.url = url;
    }

    get getUrl() {
        return this.url;
    }
}
