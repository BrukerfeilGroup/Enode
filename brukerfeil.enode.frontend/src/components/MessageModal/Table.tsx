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
                    <th>Title</th>
                    <td>
                        {props.message.difiMessage
                            ? props.message.difiMessage.messageTitle
                                ? props.message.difiMessage.messageTitle
                                : 'Ingen data'
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>External ID</th>
                    <td>
                        {props.message.elementsMessage
                            ? props.message.elementsMessage.externalId
                                ? props.message.elementsMessage.externalId
                                : 'Ingen data'
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Sender identifier</th>
                    <td>
                        {props.message.difiMessage
                            ? props.message.difiMessage.senderIdentifier
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Message ID</th>
                    <td>
                        {props.message.difiMessage
                            ? props.message.difiMessage.messageId
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Conversation ID</th>
                    <td>
                        {props.message.elementsMessage
                            ? props.message.elementsMessage.conversationId
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Status (Difi)</th>
                    <td>
                        {props.message.difiMessage
                            ? props.message.difiMessage.latestMessageStatus
                                ? props.message.difiMessage.latestMessageStatus
                                : 'Ingen data'
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Status (Elements)</th>
                    <td>
                        {props.message.elementsMessage
                            ? props.message.elementsMessage.sendingStatus
                                ? props.message.elementsMessage.sendingStatus.description
                                : 'Ingen data'
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Created (Difi)</th>
                    <td>
                        {props.message.difiMessage
                            ? props.message.difiMessage.created
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Created (Elements)</th>
                    <td>
                        {props.message.elementsMessage
                            ? props.message.elementsMessage.createdDate
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Expiry</th>
                    <td>
                        {props.message.difiMessage
                            ? props.message.difiMessage.expiry
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Type</th>
                    <td>
                        {props.message.difiMessage
                            ? props.message.difiMessage.serviceIdentifier
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Sending method</th>
                    <td>
                        {props.message.elementsMessage
                            ? props.message.elementsMessage.sendingMethod
                                ? props.message.elementsMessage.sendingMethod?.description
                                : 'Ingen data'
                            : 'Ingen data'}
                    </td>
                </tr>

                <tr>
                    <th>Name/Organization</th>
                    <td>
                        {props.message.elementsMessage
                            ? !props.message.elementsMessage.isPerson
                                ? props.message.elementsMessage.name
                                : 'Skjult'
                            : 'Ingen data'}
                    </td>
                </tr>

                <tr>
                    <th>Case ID</th>
                    <td>
                        {props.message.elementsMessage
                            ? props.message.elementsMessage.registryEntry
                                ? props.message.elementsMessage.registryEntry.case.id
                                : 'Ingen data'
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Public title</th>
                    <td>
                        {props.message.elementsMessage
                            ? props.message.elementsMessage.registryEntry
                                ? props.message.elementsMessage.registryEntry.case
                                      .publicTitle
                                : 'Ingen data'
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Case date</th>
                    <td>
                        {props.message.elementsMessage
                            ? props.message.elementsMessage.registryEntry
                                ? props.message.elementsMessage.registryEntry.case
                                      .createdDate
                                : 'Ingen data'
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Case number</th>
                    <td>
                        {props.message.elementsMessage
                            ? props.message.elementsMessage.registryEntry
                                ? props.message.elementsMessage.registryEntry.case
                                      .caseNumber
                                : 'Ingen data'
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Number of registry entries</th>
                    <td>
                        {props.message.elementsMessage
                            ? props.message.elementsMessage.registryEntry
                                ? props.message.elementsMessage.registryEntry.case
                                      .countOfRegistryEntries
                                : 'Ingen data'
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Registry entry ID</th>
                    <td>
                        {props.message.elementsMessage
                            ? props.message.elementsMessage.registryEntryId
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Registry entry title</th>
                    <td>
                        {props.message.elementsMessage
                            ? props.message.elementsMessage.registryEntry
                                ? props.message.elementsMessage.registryEntry.title
                                : 'Ingen data'
                            : 'Ingen data'}
                    </td>
                </tr>
                <tr>
                    <th>Registry entry sender recipient</th>
                    <td>
                        {props.message.elementsMessage
                            ? props.message.elementsMessage.registryEntry
                                ? props.message.elementsMessage.registryEntry
                                      .senderRecipient
                                : 'Ingen data'
                            : 'Ingen data'}
                    </td>
                </tr>
            </tbody>
        </table>
    )
}

export default Table
