document.addEventListener("DOMContentLoaded", fetchRepos);

let repos = [];

async function fetchRepos() {
    const response = await fetch("https://localhost:7286/api/github/repos");
    repos = await response.json();
    displayRepos(repos);
}

function displayRepos(repoList) {
    const ul = document.getElementById("repoList");
    ul.innerHTML = "";

    repoList.forEach(repo => {
        const li = document.createElement("li");
        li.innerHTML = `<a href="${repo.html_url}" target="_blank">${repo.name}</a> 
                        ⭐ ${repo.stargazers_count} | 🍴 ${repo.forks_count} | 🏷️ ${repo.language || "Bilinmiyor"}`;
        ul.appendChild(li);
    });
}

function filterRepos() {
    let searchQuery = document.getElementById("search").value.toLowerCase();
    let selectedLanguage = document.getElementById("languageFilter").value;

    let filteredRepos = repos.filter(repo =>
        repo.name.toLowerCase().includes(searchQuery) &&
        (selectedLanguage === "" || repo.language === selectedLanguage)
    );

    displayRepos(filteredRepos);
}

function sortRepos() {
    let sortOrder = document.getElementById("sortOrder").value;

    let sortedRepos = [...repos].sort((a, b) =>
        sortOrder === "stars" ? b.stargazers_count - a.stargazers_count : b.forks_count - a.forks_count
    );

    displayRepos(sortedRepos);
}
