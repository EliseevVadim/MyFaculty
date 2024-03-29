<template>
    <v-list-group
        :group="group"
        :prepend-icon="item.icon"
        :sub-group="subGroup"
        append-icon="mdi-menu-down"
        :color="'grey darken-1'"
    >
        <template v-slot:activator>
            <v-list-item-icon
                v-if="text"
                class="v-list-item__icon--text"
                v-text="computedText"
            />

            <v-list-item-avatar
                v-else-if="item.avatar"
                class="align-self-center"
                color="white"
                contain
            >
                <v-img src="https://demos.creative-tim.com/vuetify-material-dashboard/favicon.ico"/>
            </v-list-item-avatar>

            <v-list-item-content>
                <v-list-item-title v-text="item.title"/>
            </v-list-item-content>
        </template>

        <template v-for="(child, i) in children">
            <ItemSubGroup
                v-if="child.children"
                :key="`sub-group-${i}`"
                :item="child"
            />

            <Item
                v-else
                :key="`item-${i}`"
                :item="child"
                text
            />
        </template>
    </v-list-group>
</template>

<script>

import ItemSubGroup from "@/components/AccountComponents/base/ItemSubGroup";
import Item from "@/components/AccountComponents/base/Item";

export default {
    name: "ItemGroup",
    components: {Item, ItemSubGroup},
    props: {
        item: {
            type: Object,
            default: () => ({
                avatar: undefined,
                group: undefined,
                title: undefined,
                children: [],
            }),
        },
        subGroup: {
            type: Boolean,
            default: false,
        },
        text: {
            type: Boolean,
            default: false,
        },
    },
    methods: {
        genGroup(children) {
            return children
                .filter(item => item.to)
                .map(item => {
                    const parent = item.group || this.item.group
                    let group = `${parent}/${kebabCase(item.to)}`
                    if (item.children) {
                        group = `${group}|${this.genGroup(item.children)}`
                    }
                    return group
                }).join('|')
        },
    },
    computed: {
        children() {
            return this.item.children.map(item => ({
                ...item,
                to: !item.to ? undefined : `${this.item.group}/${item.to}`,
            }))
        },
        computedText() {
            if (!this.item || !this.item.title) return ''
            let text = ''
            this.item.title.split(' ').forEach(val => {
                text += val.substring(0, 1)
            })
            return text
        },
        group() {
            return this.genGroup(this.item.children)
        },
    },
}
</script>

<style scoped>

</style>