import { LogLevel } from '@microsoft/signalr';

export class SignalRService {
    connection = null;
    connected = false;

    invokeQueue = [];
    successQueue = [];

    constructor(options, connectionBuilder) {
        connectionBuilder.withUrl(
            this.buildUrl(options),
            options.withUrlOptions
        );
        if (options.automaticReconnect)
            connectionBuilder.withAutomaticReconnect();
        if (options.logger)
            connectionBuilder.configureLogging(LogLevel.Information);
        this.connection = connectionBuilder.build();
        this.connection.onclose(() => this.fail());
    }
    buildUrl(options) {
        console.log(options);
        return `${options.url}ws/${options.provider}`;
    }
    init() {
        this.connection
            .start()
            .then(() => {
                this.connected = true;
                while (this.invokeQueue.length) {
                    const action = this.invokeQueue.shift();
                    action.call(this);
                }

                while (this.successQueue.length) {
                    const action = this.successQueue.shift();
                    action.call(null);
                }
            })
            .catch(() => {
                this.fail();
            });
    }

    connectionSuccess(callback) {
        if (this.connected) {
            callback();
        } else {
            this.successQueue.push(callback);
        }
    }

    invoke(target, message) {
        return new Promise((res, rej) => {
            if (this.connected) {
                this.connection.invoke(target, message).then(res).catch(rej);
            } else {
                this.invokeQueue.push(() =>
                    this.connection.invoke(target, message).then(res).catch(rej)
                );
            }
        });
    }

    send(target, message) {
        if (this.connected) {
            this.connection.send(target, message);
        } else {
            this.invokeQueue.push(() => this.connection.send(target, message));
        }
    }

    on(target, callback) {
        this.connection.on(target, callback);
    }

    off(target, callback) {
        if (callback) {
            this.connection.off(target, callback);
        } else {
            this.connection.off(target);
        }
    }

    fail() {
        this.options.disconnected?.call(null);
    }
}
