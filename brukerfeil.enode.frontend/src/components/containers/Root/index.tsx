import React from 'react'
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom'
import { MessagesPageProps } from '../MessagesPage'
import Statistics from '../Statistics'
import OrganizationSelectPage from '../OrganizationSelectPage'
import MessagesPage from '../MessagesPage'

const orgId = '/:id'
const renderPage = ({ match }: MessagesPageProps) => {
    return <MessagesPage match={match} />
}

const Root: React.FC = () => {
    return (
        <Router>
            <Switch>
                <Route exact path={orgId + '/stats'} component={Statistics} />
                <Route exact path={orgId} render={renderPage} />
                <Route path="/" component={OrganizationSelectPage} />
            </Switch>
        </Router>
    )
}

export default Root
