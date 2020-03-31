export class SignalRMessage {
    /**
     *
     */
    constructor(message,serializeddata) {
        this.Message = message;
        this.SerializedData = serializeddata;
    }
    public Message : string;
    public SerializedData : string
}