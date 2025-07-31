const API_BASE_URL = "http://129.148.42.39:5000"

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
                window.location.replace("login.html");
            } else {
                alert("❌ " + baseResponse.message);
            }

        } catch (error) {
            console.error("Erro:", error);
        }

    });
}

const tokenJwt = localStorage.getItem('jwt');

(async () => {
  if (tokenJwt) {
    await validarToken();
  }
})();

async function validarToken() {
    try {
    const response = await fetch(`${API_BASE_URL}/v1/auth/validate`, {
        method: "GET",
        headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${tokenJwt}`,
        },
    });

    const baseResponse = await response.json();

    if (baseResponse.success) {
        window.location.replace("home.html");
    } else {
        localStorage.removeItem("jwt");
    }
    } catch (error) {
        console.error("Erro ao validar token:", error);
        localStorage.removeItem("jwt");
    }
}