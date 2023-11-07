import { action, makeObservable, observable } from "mobx";

export class NavigationStore {
    @observable
    history: string[] = [];

    constructor() {
        makeObservable(this);
    }

    @action
    addHistory(url: string) {
        this.history.push(url);
    }

    @action
    popHistory() {
        this.history.pop();
    }
}
