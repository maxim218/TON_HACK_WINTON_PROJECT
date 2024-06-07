"use strict";

const LinkRace = "https://komax.space/BUILD_GAME_RACE/";
const LinkArchery = "https://komax.space/BUILD_GAME_AIMING/";

let GAME_NAME_LINK = null;

function getById(id) {
    return document.getElementById(id);
}

function int(value) {
    return parseInt(value) || 0;
}

function setRandomOnlineUsers() {
    const label = getById("label-users-online-id");
    const a = Math.random() + Math.random() + Math.random() + Math.random();
    const b = a * 7000000;
    label.innerHTML = int(b);
}

function getYourMoney() {
    try {
        const jsonString = localStorage.getItem("YOUR_MONEY");
        const obj = JSON.parse(jsonString);
        return int(obj['money']);
    } catch (err) {
        return 0;
    }
}

function renderMoneyValue() {
    checkMoneyObject();
    const money = getYourMoney();
    const label = getById("label-your-money-id");
    label.innerHTML = int(money);
}

function modifyMoney(delta) {
    const oldMoney = getYourMoney();
    const newMoney = int(oldMoney) + int(delta);
    const jsonString = JSON.stringify({money: int(newMoney)});
    localStorage.setItem("YOUR_MONEY", jsonString);
}

function checkMoneyObject() {
    try {
        const jsonString = localStorage.getItem("YOUR_MONEY");
        const obj = JSON.parse(jsonString);
        const money = int(obj['money']);
    } catch (err) {
        const jsonString = JSON.stringify({money: 1500});
        localStorage.setItem("YOUR_MONEY", jsonString);
    }
}

function checkUserLogin() {
    try {
        const login = localStorage.getItem("USER_LOGIN").toString();
    } catch (err) {
        const login = "user" + int(Math.random() * 100000);
        localStorage.setItem("USER_LOGIN", login);
    }
}

function renderLoginValue() {
    const login = localStorage.getItem("USER_LOGIN").toString();
    const label = getById("label-your-login-name-id");
    label.innerHTML = login;
}

function playGameRace() {
    console.log("Play Game - Race");
    const block = getById("choose-card-price-box");
    block.style.display = "block";
    getById("label-game-chosen-name").innerHTML = "Car Race";
    getById("image-id-chosen-game").src = "/winton/images/CarRace.jpg";
    GAME_NAME_LINK = LinkRace;
}

function playGameArchery() {
    console.log("Play Game - Archery");
    const block = getById("choose-card-price-box");
    block.style.display = "block";
    getById("label-game-chosen-name").innerHTML = "Archery";
    getById("image-id-chosen-game").src = "/winton/images/Archery.png";
    GAME_NAME_LINK = LinkArchery;
}

let REVENUE = 0;

function runGameByPrice(entryFee, revenue) {
    modifyMoney(-1 * entryFee);
    console.log(`entryFee: ${entryFee} revenue: ${revenue}`);
    REVENUE = int(revenue);
    const block = getById("choose-card-price-box");
    block.style.display = "none";
    buildIframeComponent();
}

function buildIframeComponent() {
    const htmlText = `<iframe class="class-iframe-block" src="${GAME_NAME_LINK}"></iframe>`;
    const box = getById("box-for-iframe-box-id");
    box.style.display = "block";
    box.innerHTML = htmlText.trim();
}

window.onmessage = (myEvent) => {
    setTimeout(() => {
        if (myEvent.data) {
            const content = `${myEvent.data}`.trim();
            if ("WIN" === content) winGame();
            if ("LOSE" === content) loseGame();
            if ("EQUAL" === content) equalGame();
        }
    }, 2500);
};

function winGame() {
    modifyMoney(REVENUE);
    const box = getById("box-for-iframe-box-id");
    box.innerHTML = "";
    box.style.display = "none";
    renderGameResult("/winton/images/ResultWin.jpg", "You win!");
    initAndRenderMoneyLogin();
}

function loseGame() {
    const box = getById("box-for-iframe-box-id");
    box.innerHTML = "";
    box.style.display = "none";
    renderGameResult("/winton/images/ResultLoseEqual.jpg", "You lose.");
    initAndRenderMoneyLogin();
}

function equalGame() {
    const box = getById("box-for-iframe-box-id");
    box.innerHTML = "";
    box.style.display = "none";
    renderGameResult("/winton/images/ResultLoseEqual.jpg", "Draw.");
    initAndRenderMoneyLogin();
}

function renderGameResult(imageLink, textContent) {
    const box = getById("box-game-result-info-div");
    box.style.display = "block";
    box.innerHTML = `
        <div align="center">
            <img src="${imageLink}">
            <div class="label-game-result-finishing-game">${textContent}</div>
            <div class="finish-button-fin-class" onclick="gameFinishedContinue()">Continue</div>
        </div>
    `;
    initAndRenderMoneyLogin();
}

function gameFinishedContinue() {
    const box = getById("box-game-result-info-div");
    box.innerHTML = "";
    box.style.display = "none";
    initAndRenderMoneyLogin();
}

function initAndRenderMoneyLogin() {
    checkUserLogin();
    checkMoneyObject();
    setRandomOnlineUsers();
    renderMoneyValue();
    checkUserLogin();
    renderLoginValue();
}

initAndRenderMoneyLogin();

initAndRenderMoneyLogin();
