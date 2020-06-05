import React from 'react'
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom'
import Statistics from '../Statistics'
import OrganizationSelectPage from '../OrganizationSelectPage'
import MessagesPage from '../MessagesPage'

const Root: React.FC = () => {
    return (
        <Router>
            <Switch>
                <Route exact path={'/enode-frontend/:orgId'} component={MessagesPage} />
                <Route
                    exact
                    path={'/enode-frontend/:orgId/stats'}
                    component={Statistics}
                />
                <Route path="/enode-frontend/" component={OrganizationSelectPage} />
            </Switch>
        </Router>
    )
}

export default Root
