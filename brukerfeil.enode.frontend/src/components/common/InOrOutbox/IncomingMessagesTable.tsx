import React from 'react'
import Message from '../../../types/Message'
import classnames from 'classnames'
import NoMessagesRow from './NoMessagesRow'
import { parseDate, difiStatusChecker } from '../../../utils/utils'
import styles from './styles.module.css'

type MessageTableProps = {
    messages: Message[]
    onChangeActive: (id: string) => void
}

const IncomingMessagesTable: React.FC<MessageTableProps> = props => {
    return (
        <table className={styles.table}>
            <thead>
                <tr className={styles.tableHeader}>
                    <th>ID</th>
                    <th>Avsender</th>
                    <th>Tid opprettet</th>
                    <th>Sist oppdatert</th>
                    <th>Type</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody className={styles.tbody}>
                {props.messages.length > 0 ? (
                    props.messages.map(m => {
                        const lastUpdateDate = parseDate(
                            new Date(m.difiMessage.lastUpdate)
                        )
                        const createdDate = parseDate(new Date(m.difiMessage.created))
                        const difiStatusStyle = difiStatusChecker(
                            m.difiMessage.latestMessageStatus
                        )
                        return (
                            <tr
                                key={m.difiMessage.messageId}
                                className={styles.tablerow}
                                onClick={() =>
                                    props.onChangeActive(m.difiMessage.messageId)
                                }
                            >
                                <td>{m.difiMessage.messageId}</td>
                                <td>{m.difiMessage.senderIdentifier}</td>
                                <td>{createdDate}</td>
                                <td>{lastUpdateDate}</td>
                                <td>{m.difiMessage.serviceIdentifier}</td>
                                <td
                                    className={classnames(
                                        styles[difiStatusStyle],
                                        styles.bold
                                    )}
                                >
                                    {m.difiMessage.latestMessageStatus}
                                </td>
                            </tr>
                        )
                    })
                ) : (
                    <NoMessagesRow direction="in" cols={6} />
                )}
                {props.children}
            </tbody>
        </table>
    )
}

export default IncomingMessagesTable
