import React from 'react'
import SearchSenderReceiver from '../../containers/SearchSenderReceiver'
import { FaFilter } from 'react-icons/fa/'
import { TiThMenu } from 'react-icons/ti'
import useOrganizations from '../../../hooks/useOrganizations'
import styles from './styles.module.css'
import Message from '../../../types/Message'
import MessageModal from '../../MessageModal'

type NavProps = {
    orgId: string
    toggleHamburger: () => void
    toggleFilter: () => void
    onSearch: (messageId: string) => void
    onClear: () => void
    onClose: () => void
    filteredOrgId?: string
    message?: Message
}

const Navbar: React.FC<NavProps> = props => {
    const { organizations } = useOrganizations()

    const chosenOrg = organizations.find(org => org.orgId === Number(props.orgId))

    return (
        <div className={styles.container}>
            <div>
                <TiThMenu
                    className={styles.hamburger}
                    onClick={() => props.toggleHamburger()}
                />
            </div>

            <h1 className={styles.brand}>Enode</h1>
            <h2 className={styles.organization}>{chosenOrg?.orgName}</h2>
            <div className={styles.searchContent}>
                <SearchSenderReceiver
                    placeholder={'Søk på ID'}
                    onSearch={messageId => props.onSearch(messageId)}
                    onClear={props.onClear}
                    filteredOrgId={props.filteredOrgId}
                    styling={styles}
                />
                {props.message ? (
                    <MessageModal message={props.message} onCloseModal={props.onClose} />
                ) : null}
            </div>
            <FaFilter className={styles.filter} onClick={() => props.toggleFilter()} />
        </div>
    )
}

export default Navbar
