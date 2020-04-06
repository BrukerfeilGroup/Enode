export const getBackendBaseUri = () => {
    if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
        return 'http://localhost:5003'
    } else {
        return process.env.BACKEND_URL
    }
}
//"BackendURL": "https://cloud.elements-ecm.no/enode-service"

export const getFontendBaseUri = () => {
    if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') {
        return 'http://localhost:3000'
    } else {
        //Insert URL frontend will be hosted on
        return ''
    }
}
export const MESSAGES_IN_ENDPOINT = '/message/in'

export const MESSAGES_OUT_ENDPOINT = '/message/out'

export const MESSAGE_STATUS_ENDPOINT = '/messagestatus'

export const ORGANIZATION_ENDPOINT = '/organizations'
