import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'test-hero',
    templateUrl: 'hero.component.html'
})

export class HeroComponent implements OnInit {
    @Input() title = 'Hello world';

    constructor() { }

    ngOnInit() { }
}