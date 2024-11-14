window.MainPage = {
    async getMessage(): Promise<void> {
        const response = await fetch("/books/messages",
        {
            mode: "cors",
            method: "GET"
        });
        console.log(await response.json());    
    },
    async searchBooks(): Promise<void> {
        const response = await fetch("/books?languageIds=1&languageIds=2&languageIds=3&languageIds=4&languageIds=5",
        {
            mode: "cors",
            method: "GET",
        });
        console.log(await response.json());  
    },
    async searchBooks2(): Promise<void> {
        const response = await fetch("/books2?languageIds=1&languageIds=2&languageIds=3&languageIds=4&languageIds=5",
        {
            mode: "cors",
            method: "GET",
        });
        console.log(await response.json());  
    }
}
