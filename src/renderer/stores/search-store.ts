import { action, makeObservable, observable } from "mobx";

export class SearchStore {
    @observable url = "";

    constructor() {
        makeObservable(this);
    }

    @action
    setUrl(url: string) {
        this.url = url;
    }
}
