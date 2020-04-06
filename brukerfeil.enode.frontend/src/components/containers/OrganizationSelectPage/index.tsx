import React from 'react'
import OrganizationList from '../../containers/OrganizationList'
import styles from './styles.module.css'

const Frontpage: React.FC = () => {
    return (
        <div className={styles.background}>
            <div className={styles.organizationSelectionContainer}>
                <h1 className={styles.brand}>Enode</h1>
                <OrganizationList />
            </div>
        </div>
    )
}

export default Frontpage
