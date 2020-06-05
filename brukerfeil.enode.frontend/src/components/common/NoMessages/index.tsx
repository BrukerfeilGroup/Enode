import React from 'react'
import styles from './styles.module.css'

type NoMessagesRowProps = {
    direction: string
}

const NoMessages: React.FC<NoMessagesRowProps> = props => {
    return (
        <div className={styles.container}>
            <span className={styles.content}>
                Fant ingen {props.direction === 'in' ? 'innkommende ' : 'utgående '}
                meldinger
            </span>
        </div>
    )
}

export default NoMessages
