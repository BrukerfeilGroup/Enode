import { ElementsMessage } from './ElementsMessage'
import { DifiMessage } from './DifiMessage'

type Message = {
    //Difi data
    difiMessage: DifiMessage
    //Elements data
    elementsMessage: ElementsMessage
}

export default Message
