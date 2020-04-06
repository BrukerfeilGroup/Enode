import { useRef, useEffect } from 'react'

export default <T extends unknown>(content: T) => {
    const ref = useRef<T>()
    useEffect(() => {
        ref.current = content
    }, [content])
    return ref.current
}
