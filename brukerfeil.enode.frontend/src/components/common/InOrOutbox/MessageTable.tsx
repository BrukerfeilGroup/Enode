import React from 'react'
import Message from '../../../types/Message'
import { statusChecker, parseDate } from '../../../utils/utils'
import classnames from 'classnames'
import styles from './styles.module.css'

type MessageTableProps = {
    dirCheck: boolean
    messages: Message[]
    onChangeActive: (id: string) => void
}

const MessageTable: React.FC<MessageTableProps> = props => {
    return (
        <table>
            <thead>
                <tr className={styles.tableHeader}>
                    <th>ID</th>
                    <th>{props.dirCheck ? 'Avsender' : 'Mottaker'}</th>
                    <th>Tid opprettet</th>
                    <th>Sist oppdatert</th>
                    <th>Type</th>
                    <th>Siste status</th>
                </tr>
            </thead>

            <tbody>
                {props.messages
                    ? props.messages.map((m: Message) => {
                          const firstDateObject = new Date(
                              m.difiMessage
                                  ? m.difiMessage.created
                                  : m.elementsMessage.createdDate
                          )
                          const lastDateObject = new Date(
                              m.difiMessage
                                  ? m.difiMessage.lastUpdate
                                  : '2020-01-01T01:01:01.001+01:00'
                          )
                          const { style } = statusChecker(
                              m.difiMessage ? m.difiMessage.latestMessageStatus : 'ANNET'
                          )
                          const styling = classnames(styles[style], styles.bold)
                          return (
                              <tr
                                  className={styles.tablerow}
                                  onClick={() =>
                                      props.onChangeActive(
                                          m.difiMessage
                                              ? m.difiMessage.messageId
                                              : m.elementsMessage.conversationId
                                      )
                                  }
                                  key={
                                      m.difiMessage
                                          ? m.difiMessage.id
                                          : m.elementsMessage.id
                                  }
                              >
                                  <td>
                                      {m.difiMessage
                                          ? m.difiMessage.conversationId.slice(0, 18)
                                          : m.elementsMessage.conversationId.slice(0, 18)}
                                  </td>
                                  <td>
                                      {props.dirCheck
                                          ? [
                                                m.difiMessage
                                                    ? m.difiMessage.senderIdentifier
                                                    : null,
                                            ]
                                          : [
                                                m.difiMessage
                                                    ? m.difiMessage.receiverIdentifier
                                                    : null,
                                            ]}
                                  </td>
                                  <td>{parseDate(firstDateObject)}</td>
                                  <td>{parseDate(lastDateObject)}</td>
                                  <td>
                                      {m.difiMessage
                                          ? m.difiMessage.serviceIdentifier
                                          : null}
                                  </td>
                                  <td className={styling}>
                                      {m.difiMessage
                                          ? m.difiMessage.latestMessageStatus
                                          : null}
                                  </td>
                              </tr>
                          )
                      })
                    : null}
            </tbody>
        </table>
    )
}

export default MessageTable
