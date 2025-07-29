const API_BASE_URL = "http://129.148.42.39/5000"


console.log("API_BASE_URL:", API_BASE_URL);

const loginForm = document.getElementById("loginForm");

if (loginForm)
{
    const emailInput = document.getElementById("loginEmail");
    const passwordInput = document.getElementById("loginPassword");

    loginForm.addEventListener("submit", async (e) => {
        e.preventDefault();

        const emailValue = emailInput.value;
        const passwordValue = passwordInput.value;

        try {
            const response = await fetch(`${API_BASE_URL}/v1/login`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    email: emailValue,
                    password: passwordValue
                })
            });

            const baseResponse = await response.json();

            console.log(baseResponse.success);
            console.log(baseResponse.message);
            console.log(baseResponse.data);
            console.log(baseResponse.errors);

            if (baseResponse.success) {
                alert("✅ " + baseResponse.message);
            } else {
                alert("❌ " + baseResponse.message);
            }
        } catch (error) {
            console.error("Erro:", error);
        }
    })
}



const registerForm = document.getElementById("registerForm")

if (registerForm)
{
    const firstName = document.getElementById("firstName");
    const lastName = document.getElementById("lastName")
    const email = document.getElementById("email")
    const password = document.getElementById("password")

    registerForm.addEventListener("submit", async (e) => {
        e.preventDefault();

        let firstNameValue = firstName.value;
        let lastNameValue = lastName.value;
        let emailValue = email.value;
        let passwordValue = password.value;

        try {
            var response = await fetch(`${API_BASE_URL}/v1/register`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    firstName: firstNameValue,
                    lastName: lastNameValue,
                    email: emailValue,
                    password: passwordValue
                })
            });
            
            const baseResponse = await response.json();

            console.log(baseResponse.success);
            console.log(baseResponse.message);
            console.log(baseResponse.data);
            console.log(baseResponse.errors);

            if (baseResponse.success) {
                alert("✅ " + baseResponse.message);
            } else {
                alert("❌ " + baseResponse.message);
            }

        } catch (error) {
            console.error("Erro:", error);
        }

    });
}