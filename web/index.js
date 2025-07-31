const API_BASE_URL = "/v1"

const tokenJwt = localStorage.getItem('jwt');

(async () => {
  if (tokenJwt) {
    await validarToken();
  }else {
    window.location.replace("login.html")
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
        window.location.replace("login.html")
    }
    } catch (error) {
        console.error("Erro ao validar token:", error);
        localStorage.removeItem("jwt");
        window.location.replace("login.html");
    }
}