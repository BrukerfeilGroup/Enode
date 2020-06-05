export const getBackendBaseUri = () => {
    if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
        return 'http://localhost:5000'
    } else if (process.env.REACT_APP_API_URL && process.env.NODE_ENV === 'production') {
        return process.env.REACT_APP_API_URL
    } else {
        return 'https://cloud.elements-ecm.no/enode-service'
    }
}

export const MESSAGES_IN_ENDPOINT = '/messages/in'

export const MESSAGES_OUT_ENDPOINT = '/messages/out'

export const ORGANIZATION_ENDPOINT = '/organizations'
