const API_BASE_URL = "/v1"

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
            const response = await fetch(`${API_BASE_URL}/login`, {
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
                localStorage.setItem('jwt', baseResponse.data)
                alert("✅ " + baseResponse.message);
                window.location.replace("home.html");
            } else {
                alert("❌ " + baseResponse.message);
            }
        } catch (error) {
            console.error("Erro:", error);
            alert("Erro ao tentar fazer login. Tente novamente.");
        }
    })
}

const tokenJwt = localStorage.getItem('jwt');

(async () => {
  if (tokenJwt) {
    await validarToken();
  }
})();

async function validarToken() {
    try {
    const response = await fetch(`${API_BASE_URL}/auth/validate`, {
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