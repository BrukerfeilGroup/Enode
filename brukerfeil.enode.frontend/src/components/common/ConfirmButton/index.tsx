import React from 'react'
import styles from './styles.module.css'

type ConfirmButton = {
    text: string
    onClick: () => void
}

const ConfirmButton: React.FC<ConfirmButton> = props => {
    return (
        <button onClick={props.onClick} className={styles.confirmButton}>
            {props.text}
        </button>
    )
}

export default ConfirmButton
