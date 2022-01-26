import { inject } from 'vue';
import { SignalRService } from './service';
import { SignalRSymbol } from './symbols';
import { HubConnectionBuilder } from '@microsoft/signalr';

export const VueSignalR = {
    install(app, options) {
        const service = new SignalRService(options, new HubConnectionBuilder());
        console.log('signalRGame');
        app.provide('signalRGame', service);

        service.init();
    },
};

export function useSignalR() {
    const signalr = inject(SignalRSymbol);

    if (!signalr) {
        throw new Error('Failed to inject SignalR');
    }

    return signalr;
}
