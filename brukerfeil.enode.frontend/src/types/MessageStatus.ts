import { Status } from './Message'

type MessageStatus = {
    id: string
    conversationId: string
    messageId: string
    status: Status
    lastUpdate: Date
}

export default MessageStatus
