export const getBackendBaseUri = () => {
    if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
        return 'https://localhost:5003'
    } else {
        return process.env.BACKEND_URL
    }
}
//"BackendURL": "https://cloud.elements-ecm.no/enode-service"

export const MESSAGES_IN_ENDPOINT = '/messages/in'

export const MESSAGES_OUT_ENDPOINT = '/messages/out'

export const ORGANIZATION_ENDPOINT = '/organizations'
