﻿function getAjaxRequestById(url, id) {
    return new Promise((resolve, reject) => {
        fetch(`${window.host ? window.host: ''}${url}/${id}`)
            .then(response => response.json())
            .then(data => {
                resolve(data);
            })
            .catch(error => {
                console.log('Bir hata oluştu:', error);
                reject(error);
            });
    });
}

function getAjaxRequest(url) {
    return new Promise((resolve, reject) => {
        fetch(`${window.host ? window.host: ''}${url}`)
            .then(response => response.json())
            .then(data => {
                resolve(data);
            })
            .catch(error => {
                console.log('Bir hata oluştu:', error);
                reject(error);
            });
    });
}