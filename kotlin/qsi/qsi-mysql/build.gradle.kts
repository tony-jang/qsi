plugins {
    id("java")
    antlr
    kotlin("jvm")
}

group = "com.querypie.qsi.mysql"
version = "1.0-SNAPSHOT"

repositories {
    mavenCentral()
}

dependencies {
    antlr("org.antlr:antlr4:4.7.1")
    testImplementation(platform("org.junit:junit-bom:5.10.0"))
    testImplementation("org.junit.jupiter:junit-jupiter")
    implementation(kotlin("stdlib-jdk8"))
    implementation(project(":qsi-core"))
}

tasks.generateGrammarSource {
    arguments = listOf("-package", "com.querypie.qsi.mysql.antlr")
}

sourceSets {
    main {
        java {
            srcDir(tasks.generateGrammarSource)
        }
    }
}

tasks.test {
    useJUnitPlatform()
}
kotlin {
    jvmToolchain(21)
}
