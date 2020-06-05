import React from 'react'
import Message from '../../../types/Message'
import classnames from 'classnames'
import NoMessages from '../NoMessages'
import { parseDate, difiStatusChecker } from '../../../utils/utils'
import styles from './styles.module.css'
import TableSkeleton from './TableSkeleton'
import { DifiMessageKeys, DifiSortingState } from '../../../types/SortingTypes'

type MessageTableProps = {
    messages: Message[]
    difiSortingStates: DifiSortingState
    onSortDifi: (column: DifiMessageKeys) => void
    isFetching: boolean
    onChangeActive: (id: string) => void
}

const IncomingMessagesTable: React.FC<MessageTableProps> = props => {
    const getArrow = (column: DifiMessageKeys) => {
        if (props.difiSortingStates[column] === 'unsorted') {
            return ''
        }
        if (props.difiSortingStates[column] === 'ascending') {
            return '▼'
        } else return '▲'
    }

    return (
        <>
            {!props.isFetching ? (
                <>
                    <table className={styles.table}>
                        <thead>
                            <tr className={styles.tableHeader}>
                                <th onClick={() => props.onSortDifi('conversationId')}>
                                    ID {getArrow('conversationId')}
                                </th>
                                <th onClick={() => props.onSortDifi('senderIdentifier')}>
                                    Avsender {getArrow('senderIdentifier')}
                                </th>
                                <th onClick={() => props.onSortDifi('created')}>
                                    Tid <br />
                                    opprettet {getArrow('created')}
                                </th>
                                <th onClick={() => props.onSortDifi('lastUpdate')}>
                                    Sist <br />
                                    oppdatert {getArrow('lastUpdate')}{' '}
                                </th>
                                <th
                                    onClick={() =>
                                        props.onSortDifi('latestMessageStatus')
                                    }
                                >
                                    Status {getArrow('latestMessageStatus')}{' '}
                                </th>
                            </tr>
                        </thead>
                        <tbody className={styles.tbody}>
                            {props.messages.length > 0
                                ? props.messages.map(m => {
                                      const lastUpdateDate = parseDate(
                                          new Date(m.difiMessage.lastUpdate)
                                      )
                                      const createdDate = parseDate(
                                          new Date(m.difiMessage.created)
                                      )
                                      const difiStatusStyle = difiStatusChecker(
                                          m.difiMessage.latestMessageStatus
                                      )
                                      return (
                                          <tr
                                              key={m.difiMessage.messageId}
                                              className={styles.tablerow}
                                              onClick={() =>
                                                  props.onChangeActive(
                                                      m.difiMessage.messageId
                                                  )
                                              }
                                          >
                                              <td>{m.difiMessage.messageId}</td>
                                              <td>{m.difiMessage.senderIdentifier}</td>
                                              <td>{createdDate}</td>
                                              <td>{lastUpdateDate}</td>
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
                                : null}
                            {props.children}
                        </tbody>
                    </table>

                    {props.messages.length < 1 ? <NoMessages direction="in" /> : null}
                </>
            ) : (
                <TableSkeleton />
            )}
        </>
    )
}

export default IncomingMessagesTable
