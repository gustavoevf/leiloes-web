import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AlertService, LeilaoService, AuthenticationService } from '@/_services';
import { Usuario } from '@/_models';

@Component({ templateUrl: 'cadastroLeilao.component.html' })
export class CadastroLeilaoComponent implements OnInit {
    usuarioAtivo: Usuario;
    cadastroForm: FormGroup;
    loading = false;
    submitted = false;

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private authenticationService: AuthenticationService,
        private leilaoService: LeilaoService,
        private alertService: AlertService
    ) {}

    ngOnInit() {
        this.usuarioAtivo = this.authenticationService.currentUserValue;

        this.cadastroForm = this.formBuilder.group({
            nome: ['', Validators.required],
            valorInicial: ['', Validators.required],
            indicUsado: [false, Validators.required],
            finalizacao: ['', [Validators.required, this.validarData()]],
            responsavel: [''],
            abertura: ['']
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.cadastroForm.controls; }

    onSubmit() {
        this.submitted = true;
        this.cadastroForm.patchValue({abertura: new Date().toJSON().substring(0, 10), responsavel: this.usuarioAtivo.firstName});



        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.cadastroForm.invalid) {
            return;
        }

        this.loading = true;
        this.leilaoService.criarLeilao(this.cadastroForm.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Leilão cadastrado com sucesso!', true);
                    this.router.navigate(['/']);
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }

    validarData(): ValidatorFn {
      return (control: AbstractControl): {[key: string]: any} | null => {
        const data = new Date(control.value);
        const dataAtual = new Date();
        return data.getTime() < dataAtual.getTime() ? {'dataVencida': {value: control.value}} : (data.getTime() - dataAtual.getTime())/2628000000 > 3 ? {'dataDistante': {value: control.value}} : null;
      };
    }
}
