declare global {
    interface Window {
        MainPage: MainPageApi,
        SigninPage: SigninPageApi
    }
}
export interface MainPageApi {
    getMessage: () => void,
    searchBooks: () => void,
    searchBooks2: () => void,
}
export interface SigninPageApi {
    signin:() => void
}