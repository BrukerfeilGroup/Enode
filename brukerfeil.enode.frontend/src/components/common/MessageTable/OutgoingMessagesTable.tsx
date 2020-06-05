import React from 'react'
import Message from '../../../types/Message'
import styles from './styles.module.css'
import classnames from 'classnames'
import { parseDate, elementsStatusChecker, difiStatusChecker } from '../../../utils/utils'
import {
    DifiMessageKeys,
    ElementsMessageKeys,
    DifiSortingState,
    ElementsSortingState,
} from '../../../types/SortingTypes'
import NoMessages from '../NoMessages'
import TableSkeleton from './TableSkeleton'

type MessageTableProps = {
    messages: Message[]
    difiSortingStates: DifiSortingState
    elementsSortingStates: ElementsSortingState
    onSortElements: (column1: ElementsMessageKeys) => void
    onSortDifi: (column2: DifiMessageKeys, clear: boolean) => void
    isFetching: boolean
    onChangeActive: (id: string) => void
}

const OutgoingMessagesTable: React.FC<MessageTableProps> = props => {
    const getElementsArrow = (column: ElementsMessageKeys) => {
        if (props.elementsSortingStates[column] === 'unsorted') {
            return ''
        }
        if (props.elementsSortingStates[column] === 'ascending') {
            return '▼'
        } else return '▲'
    }

    const getDifiArrow = (column: DifiMessageKeys) => {
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
                                <th
                                    onClick={() => props.onSortElements('conversationId')}
                                >
                                    ID {getElementsArrow('conversationId')}
                                </th>
                                <th onClick={() => props.onSortElements('externalId')}>
                                    Mottaker {getElementsArrow('externalId')}
                                </th>
                                <th onClick={() => props.onSortElements('createdDate')}>
                                    Tid <br />
                                    opprettet {getElementsArrow('createdDate')}
                                </th>
                                <th onClick={() => props.onSortElements('lastUpdated')}>
                                    Sist <br />
                                    oppdatert {getElementsArrow('lastUpdated')}
                                </th>
                                <th
                                    onClick={() =>
                                        props.onSortDifi('latestMessageStatus', true)
                                    }
                                >
                                    Difi status
                                    {getDifiArrow('latestMessageStatus')}
                                </th>
                                <th onClick={() => props.onSortElements('sendingStatus')}>
                                    Elements status {getElementsArrow('sendingStatus')}
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            {props.messages.length > 0
                                ? props.messages.map(m => {
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
                                          ? difiStatusChecker(
                                                m.difiMessage.latestMessageStatus
                                            )
                                          : 'greyStatus'
                                      return (
                                          <tr
                                              key={m.elementsMessage.conversationId}
                                              className={styles.tablerow}
                                              onClick={() =>
                                                  props.onChangeActive(
                                                      m.elementsMessage.conversationId
                                                  )
                                              }
                                          >
                                              <td>{m.elementsMessage.conversationId}</td>
                                              <td>{m.elementsMessage.externalId}</td>
                                              <td>{createdDate}</td>
                                              <td>{lastUpdateDate}</td>
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
                                                  {
                                                      m.elementsMessage.sendingStatus
                                                          ?.description
                                                  }
                                              </td>
                                          </tr>
                                      )
                                  })
                                : null}

                            {props.children}
                        </tbody>
                    </table>
                    {props.messages.length < 1 && <NoMessages direction="out" />}
                </>
            ) : (
                <TableSkeleton />
            )}
        </>
    )
}

export default OutgoingMessagesTable
