const listaPets = document.getElementById("listaPets");
const totalPets = document.getElementById("totalPets");
const filtroEspecie = document.getElementById("filtroEspecie");
const filtroPorte = document.getElementById("filtroPorte");
const filtroCidade = document.getElementById("filtroCidade");
const botaoFiltrar = document.getElementById("botaoFiltrar");
const petIdConsulta = document.getElementById("petIdConsulta");
const botaoConsultar = document.getElementById("botaoConsultar");
const resultadoConsulta = document.getElementById("resultadoConsulta");
const petInteresse = document.getElementById("petInteresse");
const formInteresse = document.getElementById("formInteresse");
const retornoInteresse = document.getElementById("retornoInteresse");

async function carregarPets() {
    const parametros = new URLSearchParams();

    if (filtroEspecie.value) {
        parametros.set("especie", filtroEspecie.value);
    }

    if (filtroPorte.value) {
        parametros.set("porte", filtroPorte.value);
    }

    if (filtroCidade.value.trim()) {
        parametros.set("cidade", filtroCidade.value.trim());
    }

    const resposta = await fetch(`/api/pets?${parametros.toString()}`);
    const pets = await resposta.json();

    totalPets.textContent = `${pets.length} pet${pets.length === 1 ? "" : "s"} `;
    preencherCards(pets);
    preencherSelecaoDeInteresse(pets);
}
function preencherCards(pets) {
    listaPets.innerHTML = "";

    if (pets.length === 0) {
        listaPets.innerHTML = "<p>Nenhum pet encontrado com os filtros informados.</p>";
        return;
    }

    for (const pet of pets) {
        const card = document.createElement("article");
        card.className = "pet‑card";
        card.innerHTML = `
            <span class="icon"> ${pet.icone}</span >
                <h3>${pet.nome}</h3>
                <p>${pet.descricao}</p>
                <div class="badges">
                <span class="badge">${pet.especie}</span>
                <span class="badge">${pet.porte}</span>
                <span class="badge">${pet.idadeAproximada} ano(s)</span>
                <span class="badge">${pet.cidade}</span>
                </div >
                <small>${pet.caracteristicas.join(" • ")}</small>
`;

        listaPets.appendChild(card);
    }
}
function preencherSelecaoDeInteresse(pets) {
    petInteresse.innerHTML = "";

    for (const pet of pets) {
        const opcao = document.createElement("option");
        opcao.value = pet.id;
        opcao.textContent = `${pet.nome} – ${pet.especie} `;
        petInteresse.appendChild(opcao);
    }
}
async function consultarPetPorId() {
    const id = petIdConsulta.value;

    const resposta = await fetch(`/ api / pets / ${id} `);
    const conteudo = await resposta.json();

    if (!resposta.ok) {
        resultadoConsulta.textContent = conteudo.mensagem;
        return;
    }

    resultadoConsulta.innerHTML = `
    < strong > ${conteudo.nome}</strong > <br>
        ${conteudo.especie}, porte ${conteudo.porte}, em ${conteudo.cidade}.<br>
            ${conteudo.descricao}
            `;
}
async function enviarInteresse(evento) {
    evento.preventDefault();

    const corpo = {
        petId: Number(petInteresse.value),
        nome: document.getElementById("nomeInteresse").value,
        email: document.getElementById("emailInteresse").value,
        mensagem: document.getElementById("mensagemInteresse").value
    };

    const resposta = await fetch("/api/interesses", {
        method: "POST",
        headers: {
            "Content‑Type": "application/json"
        },
        body: JSON.stringify(corpo)
    });

    const conteudo = await resposta.json();
    retornoInteresse.textContent = conteudo.mensagem;

    if (resposta.ok) {
        formInteresse.reset();
    }
}
botaoFiltrar.addEventListener("click", carregarPets);
botaoConsultar.addEventListener("click", consultarPetPorId);
formInteresse.addEventListener("submit", enviarInteresse);

carregarPets();