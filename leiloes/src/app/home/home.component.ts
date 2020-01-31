import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { Usuario } from '@/_models';
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

    deleteUser(id: number) {
        this.userService.delete(id)
            .pipe(first())
            .subscribe();
    }

    deletarLeilao(id: number) {
        this.leilaoService.deletarLeilao(id)
            .pipe(first())
            .subscribe(() => this.obterLeiloes());
    }

    private obterLeiloes() {
      this.leilaoService.obterLeiloes()
          .pipe(first())
          .subscribe(leiloes => this.leiloes = leiloes);
    }
}
