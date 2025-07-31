const API_BASE_URL = "http://129.148.42.39:5000"

const tokenJwt = localStorage.getItem('jwt');

(async () => {
  if (tokenJwt) {
    await validarToken();
  } else {
    window.location.replace("login.html");
  }
})();

async function validarToken() {
    try {
        var response = await fetch(`${API_BASE_URL}/v1/auth/validate`, {
                method: "GET",
                headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${tokenJwt}`,
            },
        });

        const baseResponse = await response.json();
        
        if (!baseResponse.success) {
            localStorage.removeItem("jwt");
            window.location.replace("login.html");
        }
        
    } catch (error) {
        console.log(error);
        alert("Erro ao tentar validar token");
    }
}