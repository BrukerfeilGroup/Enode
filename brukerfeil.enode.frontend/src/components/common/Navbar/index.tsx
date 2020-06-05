import React, { useState } from 'react'
import Searchbar from '../../containers/Searchbar'
import { FaFilter } from 'react-icons/fa/'
import { TiThMenu } from 'react-icons/ti'
import useOrganizations from '../../../hooks/useOrganizations'
import styles from './styles.module.css'

type NavProps = {
    orgId: string
    filters: boolean
    toggleHamburger: () => void
    toggleFilter: () => void
    onFilterMessage: (input: string) => void
    onSearch: (messageId: string) => void
}

const Navbar: React.FC<NavProps> = props => {
    const { organizations } = useOrganizations()
    const [filterVisible] = useState<boolean>(props.filters)
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
            {
                <div
                    className={
                        filterVisible
                            ? styles.searchContent
                            : styles.searchContentNotvisible
                    }
                >
                    <Searchbar
                        placeholder={'Søk på ID'}
                        onSearch={messageId => props.onSearch(messageId)}
                        styling={styles}
                        onChange={props.onFilterMessage}
                        disabled={false}
                    />
                </div>
            }
            {
                <FaFilter
                    className={filterVisible ? styles.filter : styles.filterNotvisible}
                    onClick={() => props.toggleFilter()}
                />
            }
        </div>
    )
}

export default Navbar
