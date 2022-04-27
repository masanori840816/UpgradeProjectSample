export async function getMessage(): Promise<void> {
    const response = await fetch("/books/messages",
    {
        mode: "cors",
        method: "GET"
    });
    console.log(await response.json());    
}
export async function postMessage(): Promise<void> {
    const response = await fetch("/books/messages",
    {
        mode: "cors",
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            id: 3,
            name: "Hello",
            authorId: 5,
            languageId: 2,
            purchaseDateTime: new Date("2022-03-08"),
            price: 3000,
        }),
    });
    console.log(await response.json());  
}
