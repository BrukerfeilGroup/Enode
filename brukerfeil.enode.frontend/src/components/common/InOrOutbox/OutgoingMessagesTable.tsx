import React from 'react'
import Message from '../../../types/Message'
import styles from './styles.module.css'
import classnames from 'classnames'
import { parseDate, elementsStatusChecker, difiStatusChecker } from '../../../utils/utils'
import NoMessagesRow from './NoMessagesRow'
import LoadingIndicator from '../LoadingIndicator'

type MessageTableProps = {
    messages: Message[]
    //isFetching: boolean
    onChangeActive: (id: string) => void
}

const OutgoingMessagesTable: React.FC<MessageTableProps> = props => {
    return (
        <table className={styles.table}>
            <thead>
                <tr className={styles.tableHeader}>
                    <th>ID</th>
                    <th>Mottaker</th>
                    <th>Tid opprettet</th>
                    <th>Sist oppdatert</th>
                    <th>Type</th>
                    <th>Difi status</th>
                    <th>Elements status</th>
                </tr>
            </thead>
            <tbody>
                {props.messages.length > 0 ? (
                    props.messages.map(m => {
                        const difiNotNull = m.difiMessage !== null
                        const lastUpdateDate = parseDate(
                            new Date(m.elementsMessage?.lastUpdated)
                        )

                        const createdDate = parseDate(
                            new Date(m.elementsMessage.createdDate)
                        )

                        const elementsStatusStyle = elementsStatusChecker(
                            m.elementsMessage.sendingStatus?.description
                        )

                        const difiStatusStyle = difiNotNull
                            ? difiStatusChecker(m.difiMessage.latestMessageStatus)
                            : 'greyStatus'
                        return (
                            <tr
                                key={m.elementsMessage.id}
                                className={styles.tablerow}
                                onClick={() =>
                                    props.onChangeActive(m.elementsMessage.conversationId)
                                }
                            >
                                <td>{m.elementsMessage.conversationId}</td>
                                <td>{m.elementsMessage.externalId}</td>
                                <td>{createdDate}</td>
                                <td>{lastUpdateDate}</td>
                                <td>
                                    {difiNotNull
                                        ? m.difiMessage.serviceIdentifier
                                        : m.elementsMessage.sendingMethod?.description}
                                </td>
                                <td
                                    className={classnames(
                                        styles[difiStatusStyle],
                                        styles.bold
                                    )}
                                >
                                    {difiNotNull
                                        ? m.difiMessage.latestMessageStatus
                                        : 'Ingen data'}
                                </td>
                                <td
                                    className={classnames(
                                        styles[elementsStatusStyle],
                                        styles.bold
                                    )}
                                >
                                    {m.elementsMessage.sendingStatus?.description}
                                </td>
                            </tr>
                        )
                    })
                ) : (
                    <tr style={{ height: '100%' }}>
                        <NoMessagesRow direction="out" cols={7} />
                        <td colSpan={7}>{/* <LoadingIndicator size="SMALL" /> */}</td>
                    </tr>
                )}
                {props.children}
            </tbody>
        </table>
    )
}

export default OutgoingMessagesTable
