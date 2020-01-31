import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Leilao } from '@/_models';

@Injectable({ providedIn: 'root' })
export class LeilaoService {
    constructor(private http: HttpClient) { }

    obterLeiloes() {
        return this.http.get<Leilao[]>(`${config.apiUrl}/leiloes`);
    }

    criarLeilao(leilao: Leilao) {
        return this.http.post(`${config.apiUrl}/leiloes/criar`, leilao);
    }

    deletarLeilao(id: number) {
        return this.http.delete(`${config.apiUrl}/leiloes/${id}`);
    }
}
