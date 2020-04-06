import React, { useEffect, useCallback } from 'react'
import Message from '../../types/Message'
import Timeline from './Timeline'
import Table from './Table'
import styles from './styles.module.css'
//import useScrollable from '../../hooks/useScrollable'

type MessageModalProps = {
    message: Message
    onCloseModal: () => void
}

const MessageModal: React.FC<MessageModalProps> = props => {
    const escClick = useCallback(
        (event: KeyboardEvent) => {
            if (event.keyCode === 27) {
                props.onCloseModal()
            }
        },
        [props]
    )

    useEffect(() => {
        document.addEventListener('keydown', escClick, false)

        return () => {
            document.removeEventListener('keydown', escClick)
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    return (
        <div onClick={() => props.onCloseModal()} className={styles.background}>
            <div onClick={event => event.stopPropagation()} className={styles.container}>
                <button className={styles.exit} onClick={() => props.onCloseModal()}>
                    X
                </button>

                <div>
                    <h1 className={styles.header}>
                        Melding ID:{' '}
                        {props.message.difiMessage
                            ? props.message.difiMessage.messageId
                            : props.message.elementsMessage.conversationId}
                    </h1>
                </div>

                <div className={styles.content}>
                    <Timeline message={props.message} />
                    <Table message={props.message} />
                </div>
            </div>
        </div>
    )
}

export default MessageModal
