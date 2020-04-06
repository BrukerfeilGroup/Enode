import React from 'react'
import Message from '../../types/Message'
import styles from './styles.module.css'

type TableProps = {
    message: Message
}

const Table: React.FC<TableProps> = props => {
    return (
        <table className={styles.table}>
            <caption className={styles.tableHeader}>Meldingsinformasjon</caption>
            <tbody>
                <tr>
                    <th>Til</th>
                    <td>
                        {props.message.difiMessage
                            ? props.message.difiMessage.receiverIdentifier
                            : null}
                    </td>
                </tr>
                <tr>
                    <th>Fra</th>
                    <td>
                        {props.message.difiMessage
                            ? props.message.difiMessage.senderIdentifier
                            : null}
                    </td>
                </tr>
                <tr>
                    <th>Status</th>
                    <td>
                        {props.message.difiMessage
                            ? props.message.difiMessage.latestMessageStatus
                            : null}
                    </td>
                </tr>
                <tr>
                    <th>Type</th>
                    <td>
                        {props.message.difiMessage
                            ? props.message.difiMessage.serviceIdentifier
                            : null}
                    </td>
                </tr>
                <tr>
                    <th>ConvID</th>
                    <td>
                        {props.message.difiMessage
                            ? props.message.difiMessage.conversationId
                            : null}
                    </td>
                </tr>
                <tr>
                    <th>MsgID</th>
                    <td>
                        {props.message.difiMessage
                            ? props.message.difiMessage.messageId
                            : props.message.elementsMessage.conversationId}
                    </td>
                </tr>
            </tbody>
        </table>
    )
}

export default Table
