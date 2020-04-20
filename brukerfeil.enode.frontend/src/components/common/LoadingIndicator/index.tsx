import React from 'react'
import styles from './styles.module.css'

type LoadingIndicatorProps = {
    size: 'SMALL' | 'LARGE'
}

const LoadingIndicator: React.FC<LoadingIndicatorProps> = props => {
    const size = props.size === 'SMALL' ? '50px' : '100px'

    return (
        <div className={styles.loading}>
            <span style={{ display: 'block' }}>Laster inn meldinger...</span>
            <div className={styles.spinner} style={{ height: size, width: size }}></div>
        </div>
    )
}

export default LoadingIndicator
