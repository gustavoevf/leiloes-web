import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { Usuario, Leilao } from '@/_models';
import { UsuarioService, AuthenticationService, LeilaoService } from '@/_services';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit {
    currentUser: Usuario;
    users = [];
    leiloes = [];

    constructor(
        private authenticationService: AuthenticationService,
        private userService: UsuarioService,
        private leilaoService: LeilaoService
    ) {
        this.currentUser = this.authenticationService.currentUserValue;
    }

    ngOnInit() {
        this.obterLeiloes();
    }

    deletarLeilao(id: number) {
        this.leilaoService.deletarLeilao(id)
            .pipe(first())
            .subscribe(() => this.obterLeiloes());
        }

    atualizarLeilao(leilao: Leilao) {
        this.leilaoService.atualizarLeilao(leilao)
            .pipe(first())
            .subscribe(() => this.obterLeiloes());
    }

    private obterLeiloes() {
      this.leilaoService.obterLeiloes()
          .pipe(first())
          .subscribe(leiloes => this.leiloes = leiloes);
    }

    remocaoPermitida(leilao: Leilao) {
      return leilao.responsavel == this.currentUser.username;
    }

    obterStatus(data: string) {
        return new Date(data).getTime() < Date.now() ? 'Encerrado' : 'Ativo'; 
    }
}
