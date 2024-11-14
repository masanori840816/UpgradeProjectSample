import { hasAnyTexts } from "./texts/hasAnyTexts";

window.SigninPage = {
    async signin(): Promise<void> {
        const value = getSigninValue();
        if(value == null) {
            alert("Invalid e-mail or password");
            return;
        }
        const response = await fetch("/user/signin", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(value),
        });
        const result = JSON.parse(JSON.stringify(await response.json()));
        if(result.succeeded) {
            location.href = "/pages/index";
            return;
        }
        alert(result.errorMessage);
        
    }
}
function getSigninValue(): { email: string, password: string }|null {
    const emailInput = document.getElementById("signin_email") as HTMLInputElement;
    const passwordInput = document.getElementById("signin_password") as HTMLInputElement;

    if(!hasAnyTexts(emailInput.value) ||
        !hasAnyTexts(passwordInput.value)) {
        return null;
    }
    return {
        email: emailInput.value,
        password: passwordInput.value, 
    };
}