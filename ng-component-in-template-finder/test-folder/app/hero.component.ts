import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'ma-hero',
    templateUrl: 'hero.component.html'
})

export class HeroComponent implements OnInit {
    @Input() title = 'Hello world';

    constructor() { }

    ngOnInit() { }
}